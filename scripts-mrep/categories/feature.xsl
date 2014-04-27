<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:table('str_feature')">
          <feature>
            <featureid m:left="4" m:top="64">
              <xsl:value-of select="id" />
            </featureid>
            <name m:left="4" m:top="92">
              <xsl:value-of select="name" />
            </name>
            <typeid m:left="6" m:top="134">
              <xsl:value-of select="typeid" />
            </typeid>
            <ispublic m:left="4" m:top="188">
              <xsl:value-of select="ispublic" />
            </ispublic>
          </feature>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>