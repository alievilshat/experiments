<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select bpv.postvalueid, bpc.categoryid from cms_blogpostvalue bpv inner join cms_blogpost bp on bp.postid = bpv.postid inner join cms_blogpostcategories bpc on bpc.postid = bp.postid')">
          <blogpostcategory>
            <blogpostcategoryid m:left="-86" m:top="246" xmlns:m="http://www.navitas.nl/2014/Mapper">pk:blogpostcategory(<xsl:value-of select="postvalueid" />-<xsl:value-of select="categoryid" />)</blogpostcategoryid>
            <blogpostid m:left="-84" m:top="174" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="postvalueid" />
            </blogpostid>
            <blogcategoryid m:left="-85" m:top="211" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="categoryid" />
            </blogcategoryid>
          </blogpostcategory>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
  ]]></msxsl:script>
</xsl:stylesheet>