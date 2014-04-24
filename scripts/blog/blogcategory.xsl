<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select cb.*, w.businessid from cms_blogcategories cb inner join cms_website w on cb.websiteid = w.websiteid')">
          <blogcategory>
            <blogcategoryid m:left="35" m:top="163" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="blogcategoryid" />
            </blogcategoryid>
            <name m:left="-87" m:top="178" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </name>
            <businessid m:left="35" m:top="194" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="businessid" />
            </businessid>
          </blogcategory>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
  ]]></msxsl:script>
</xsl:stylesheet>