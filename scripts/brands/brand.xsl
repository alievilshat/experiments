<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_brand')">
          <brand>
            <brandid m:left="-130" m:top="40" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </brandid>
            <brandname m:left="18" m:top="180" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </brandname>
            <fullname m:left="-116" m:top="196" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="fullname" />
            </fullname>
            <website m:left="20" m:top="214" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="website" />
            </website>
            <emailsupport m:left="-116" m:top="230" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="emailsupport" />
            </emailsupport>
            <supportpage m:left="20" m:top="246" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="supportpage" />
            </supportpage>
            <supportphone m:left="-116" m:top="262" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="supportphone" />
            </supportphone>
            <inactive m:left="-38" m:top="318" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="inactived" />
            </inactive>
            <description m:left="28" m:top="284" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="description" />
            </description>
            <largedescription m:left="-38" m:top="348" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="largedescription" />
            </largedescription>
          </brand>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>